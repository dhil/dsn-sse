package dk.aau.student.dsn.selfstudy1;
import java.net.*;
import java.io.*;

public class HttpServer implements Runnable {
	// Instance variables
	private String wwwroot;
	private int port;
	private ServerSocket socket;
	
	// Constructors
	public HttpServer(String wwwroot, int port) 
			throws IOException, IllegalArgumentException {
		this.setRootDir(wwwroot)
			.setPort(port);
		// Initialize socket
		socket = initSocket(getPort());
	}
	
	public HttpServer(int port) 
			throws IOException, IllegalArgumentException {
		this(null, port);
	}
	
	// Getters & Setters
	protected HttpServer setPort(int port) throws IllegalArgumentException {
		if (port < 1)
			throw new IllegalArgumentException("Port number must be positive.");
		this.port = port; return this; 
	}
	public int getPort() { return port;	}
	public HttpServer setRootDir(String wwwroot) {
		if (wwwroot == null)
			this.wwwroot = "."; // Defaults to current working directory
		else
			this.wwwroot = wwwroot; 
		return this; 
	}
	public String getRootDir() { return wwwroot; }
	
	// Socket initializing method
	protected ServerSocket initSocket(int port) throws IOException {
		ServerSocket socket = null;
		try {
			socket = new ServerSocket(port);
			System.out.println("Listening for incoming requests on " + port + "...");
		} catch (IOException e) {
			// TODO: Log
			throw e;
		}
		return socket;
	}

	// Request handler loop
	@Override
	public void run() {
		while (true) {
			Socket connection = null;
			try {
				// Listen for incoming requests
				connection = socket.accept();
				System.out.println("Request accepted.");
				// Dispatch handler
				handleRequest(connection);
			} catch (Exception e) {
				// Suppress
			} 
		}
	}
	
	protected void handleRequest(Socket connection) {
		Thread handler = new Thread(new HttpRequestHandler(connection, wwwroot));
		handler.start();
		System.out.println("Dispatched.");
	}
}
