package dk.aau.student.dsn.selfstudy1.messaging;
import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.io.SequenceInputStream;
import java.nio.charset.Charset;

public class HttpResponse extends HttpMessage {
	// Instance variables
	private HttpStatusCode status;
	private InputStream bodyStream;
	private boolean bodyFromFile;
	
	// Constructor
	public HttpResponse() {
		this.setProtocol(HttpProtocol.Http11);
		bodyFromFile = false;
	}
	
	// Getters & Setters
	public HttpStatusCode getStatusCode() { return status; }
	public HttpResponse setStatusCode(HttpStatusCode status) { this.status = status; return this; }
	public HttpResponse setFile(InputStream stream) {
		InputStream crlf = new ByteArrayInputStream("\r\n".getBytes(Charset.forName("UTF-8")));
		this.bodyStream   = new SequenceInputStream(stream, crlf);
		this.bodyFromFile = true;
		return this;
	}
	public InputStream getAsStream() {
		InputStream headerStream = new ByteArrayInputStream(getHeaderAsString().getBytes(Charset.forName("UTF-8")));
		return bodyFromFile ? new SequenceInputStream(headerStream, this.bodyStream) : headerStream;
	}
	
	private String getHeaderAsString() {
		return getProtocol().toString() + " " + getStatusCode().toString() + " \r\n\r\n";
	}
}
