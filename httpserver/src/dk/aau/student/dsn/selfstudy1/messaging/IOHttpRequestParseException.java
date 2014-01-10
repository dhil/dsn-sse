package dk.aau.student.dsn.selfstudy1.messaging;

import java.io.IOException;

public class IOHttpRequestParseException extends HttpRequestParseException {

	/**
	 * 
	 */
	private static final long serialVersionUID = 8821729492160936581L;
	
	
	public IOHttpRequestParseException(String message, Exception innerException) {
		super(message, innerException);
	}
	
	public IOHttpRequestParseException() {
		this(null, null);
	}
}
