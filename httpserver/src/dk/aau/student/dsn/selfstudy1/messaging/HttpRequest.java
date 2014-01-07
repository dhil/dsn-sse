package dk.aau.student.dsn.selfstudy1.messaging;

public class HttpRequest extends HttpMessage {
	// Instance variables
	private HttpMethod method;
	private String uri;
	
	// Getters & setters
	public HttpMethod getMethod() { return method; }
	public HttpRequest setMethod(HttpMethod method) {
		this.method = method;
		return this;
	}
	
	public String getURI() { return uri; }
	public HttpRequest setURI(String uri) {
		this.uri = uri;
		return this;
	}
	
	public HttpRequest setProtocol(HttpProtocol protocol) {
		return (HttpRequest)super.setProtocol(protocol);
	}
	
	public String toString() {
		return getMethod().toString() + " " + getURI() + " " + getProtocol().toString() + "\r\n";
	}
}
