package dk.aau.student.dsn.selfstudy1;

import java.io.File;

public class Webserver {

	public static void main(String[] args) {
		// Variables
		int port = 0;
		String wwwroot = null;
		
		// Parse CLI arguments
		if (args.length != 2) {
			usage();
			System.exit(1);
		}
		
		wwwroot = args[0];
		if (!(new File(wwwroot)).exists()) {
			System.out.println("error: Webserver: No such directory " + wwwroot);
			System.exit(2);
		}
		
		port 	= Integer.parseInt(args[1]);
		
		// Instantiate server
		HttpServer server = null;
		try {
			server = new HttpServer(wwwroot, port);
		} catch (Exception e) {
			System.out.println("error: Webserver: Failed to start server: " + e.getMessage());
			System.exit(3);
		}
		
		// Run the server
		server.run();
	}
	
	private static void usage() {
		System.out.println("usage: java Webserver <wwwroot> <port>");
	}

}
