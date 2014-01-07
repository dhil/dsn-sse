package dk.aau.student.dsn.selfstudy1.messaging;

public enum HttpStatusCode {
	// 200s - Success
	OK {
		public String toString() {
			return "200 OK";
		}
	},Created,Accepted,
	// 400s - Client error
	BadRequest {
		public String toString() {
			return "400 Bad Request";
		}
	}, NotFound {
		public String toString() {
			return "404 Not Found";
		}
	},
	// 500s - Server error
	InternalServerError {
		public String toString() {
			return "500 Internal Server Error";
		}
	}, NotImplemented, ConnectionTimedOut
}

