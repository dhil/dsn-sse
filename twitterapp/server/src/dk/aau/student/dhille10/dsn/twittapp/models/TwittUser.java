package dk.aau.student.dhille10.dsn.twittapp.models;


import javax.xml.bind.annotation.XmlRootElement;

/*Data model for a twitt user*/

/*Tag needed to serialize to XML with JAXB*/
@XmlRootElement
public class TwittUser {
	
	/*Fields of the twitt user*/
	private String id;
	private String name;
	private String screenName;
	private String location;
	private String description;

	/*JAXB requires an empty constructor*/
	public TwittUser() { }

	/*Basic constructor*/
    public TwittUser(String id, String name, String screenName, String location, String description)
    {
        this.id = id;
        this.name = name;
        this.screenName = screenName;
        this.location = location;
        this.description = description;
    }
    
    /*Getters and setters*/
	
    public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getScreenName() {
		return screenName;
	}

	public void setScreenName(String screenName) {
		this.screenName = screenName;
	}

	public String getLocation() {
		return location;
	}

	public void setLocation(String location) {
		this.location = location;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}
}
