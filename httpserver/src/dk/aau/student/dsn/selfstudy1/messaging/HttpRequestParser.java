package dk.aau.student.dsn.selfstudy1.messaging;
import java.io.*;

public class HttpRequestParser {
	// Instance variables
	
	
	// Constructors
	public HttpRequestParser() {
		
	}
	
	// Methods
	// Simple get request parser, generalize later...
	public HttpRequest parse(InputStream requestStream) throws HttpRequestParseException, NullPointerException {
		if (requestStream == null)
			throw new NullPointerException();
		BufferedReader in = new BufferedReader(new InputStreamReader(requestStream));
		HttpRequest httpRequest = new HttpRequest();
		try {
			// Read request line
			String request = in.readLine();
			// Ignore the rest
			//while (in.readLine() != null);
			
			// Populate httpRequest object
			// parse the line
			if (!request.startsWith("GET") || request.length()<14 ||
			    !(request.endsWith("HTTP/1.0") || request.endsWith("HTTP/1.1"))) {
			    // bad request
			    throw new HttpRequestParseException("Bad request");
			} else {
				HttpProtocol protocol = request.endsWith("HTTP/1.1") ? HttpProtocol.Http11 : HttpProtocol.Http;
			    String uri = request.substring(4, request.length()-9).trim();
			    httpRequest.setMethod(HttpMethod.GET)
			    		   .setURI(uri)
			    	       .setProtocol(protocol); 
			}
		} catch (IOException e) {
			throw new IOHttpRequestParseException("I/O socket error.", e);
		} catch (Exception e) {
			throw new HttpRequestParseException("Unhandled parse exception.", e);
		}
		return httpRequest;
	}
}
