package dk.aau.student.dhille10.dsn.twittapp.models;


import javax.xml.bind.annotation.XmlRootElement;

/*Data model for a twitt status*/

/*Tag needed to serialize to XML with JAXB*/
@XmlRootElement
public class TwittStatus {
	private String id;
	private String userId;
	private String text;
	private String createdAt;
	
	public TwittStatus() {}
	
	public TwittStatus(String id, String userId, String text, String createdAt) {
		this.id = id;
		this.userId = userId;
		this.text = text;
		this.createdAt = createdAt;
	}
	
 /*Getters and setters*/
	
    public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getUserId() {
		return userId;
	}

	public void setUserId(String userId) {
		this.userId = userId;
	}

	public String getText() {
		return text;
	}

	public void setText(String text) {
		this.text = text;
	}

	public String getCreatedAt() {
		return createdAt;
	}

	public void setCreatedAt(String createdAt) {
		this.createdAt = createdAt;
	}
}