package dk.aau.student.dsn.selfstudy1.messaging;

public class HttpRequestParseException extends Exception {

	/**
	 * 
	 */
	private static final long serialVersionUID = 8109996119843808471L;

	public HttpRequestParseException(String message, Exception innerException) {
		super(message, innerException);
	}
	
	public HttpRequestParseException() {
		this(null);
	}
	
	public HttpRequestParseException(String message) {
		this(message, null);
	}
}
