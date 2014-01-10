package dk.aau.student.dsn.selfstudy1.messaging;

public enum HttpProtocol {
	Http11 {
		public String toString() { return "HTTP/1.1"; }
	},
	Http {
		public String toString() { return "HTTP"; }
	}
}
