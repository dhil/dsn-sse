package dk.aau.student.dsn.selfstudy1.messaging;

public abstract class HttpMessage {
	// Instance variables
	protected String body;
	protected HttpProtocol protocol;
	
	// Getters
	public String getBody() {
		return body;
	}
	
	public int getContentLength() {
		return body == null ? 0 : body.length();	
	}
	
	public HttpProtocol getProtocol() {
		return protocol;
	}
	
	public HttpMessage setProtocol(HttpProtocol protocol) {
		this.protocol = protocol;
		return this;
	}
}
