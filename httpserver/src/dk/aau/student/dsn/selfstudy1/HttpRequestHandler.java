package dk.aau.student.dsn.selfstudy1;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

import dk.aau.student.dsn.selfstudy1.messaging.HttpRequest;
import dk.aau.student.dsn.selfstudy1.messaging.HttpRequestParseException;
import dk.aau.student.dsn.selfstudy1.messaging.HttpRequestParser;
import dk.aau.student.dsn.selfstudy1.messaging.HttpResponse;
import dk.aau.student.dsn.selfstudy1.messaging.HttpStatusCode;

public class HttpRequestHandler implements Runnable {
	// Instance variables
	private Socket connection;
	private String wwwroot;
	// Constructor
	public HttpRequestHandler(Socket connection, String wwwroot) {
		this.connection = connection;
		this.wwwroot    = wwwroot;
	}
	
	@Override
	public void run() {
		System.out.println("Handling request...");
		HttpRequestParser requestParser = new HttpRequestParser();
		HttpResponse response 			= new HttpResponse();
		File file                       = null;
		try {
			// Parse request
			System.out.println("Parsing request...");
			HttpRequest request = requestParser.parse(connection.getInputStream());
			System.out.println("Request parsed.");
			// Sanitize (security)
			// Generate response
			if (request.getURI().endsWith("/"))
				file = new File(wwwroot + request.getURI() + "index.html");
			else
				file = new File(wwwroot + "/" + request.getURI());
			System.out.println("Request file: " + file.getAbsolutePath());
			if (!file.exists())
				throw new FileNotFoundException();
			// Attach body from file
			response.setFile(new FileInputStream(file));
			response.setStatusCode(HttpStatusCode.OK);
		} catch (HttpRequestParseException e) {
			response.setStatusCode(HttpStatusCode.InternalServerError);
		} catch (IOException e) {
			response.setStatusCode(HttpStatusCode.NotFound);
		} finally {
			try {
				System.out.println("Sending response...");
				InputStream responseStream = response.getAsStream();
				OutputStream out = connection.getOutputStream();
				byte[] buffer = new byte[1000];
				int n;
			    while ((n = responseStream.read(buffer, 0, buffer.length)) != -1) {
			    	out.write(buffer, 0, n);
			    }
				connection.close();
			} catch (Exception e) {
				// Ignore
				System.out.println("Error while responding to request: " + e.getMessage());
				e.printStackTrace(System.out);
			}
		}
	}
}
