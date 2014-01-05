package dk.aau.student.dhille10.dsn.twittapp.resources;

import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.PUT;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Request;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.UriInfo;
import javax.xml.bind.JAXBElement;

import dk.aau.student.dhille10.dsn.twittapp.models.*;
import dk.aau.student.dhille10.dsn.twittapp.storage.*;

import com.sun.jersey.api.NotFoundException;

/*Resource for TwittUser*/
/*See TwittStatus*/

public class TwittUserResource {

	@Context
	UriInfo uriInfo;
	@Context
	Request request;
	String id;

	public TwittUserResource(UriInfo uriInfo, Request request, String id) {
		this.uriInfo = uriInfo;
		this.request = request;
		this.id = id;
	}

	@GET
	@Produces(MediaType.APPLICATION_XML)
	public TwittUser getUser() {
		TwittUser tu = TwittStore.instance.getTuP().get(id);
		if(tu==null)
			throw new NotFoundException("No such TwittUser.");
		return tu;
	}

	@PUT
	@Consumes(MediaType.APPLICATION_XML)
	public Response putUserDescription(JAXBElement<TwittUser> jaxbMessage) {
		TwittUser tu = jaxbMessage.getValue();
		return putUser(tu);
	}


	private Response putUser(TwittUser tu) {
		Response res;
		if(!tu.getId().equals(id))
			tu.setId(id);
		if(TwittStore.instance.getTuP().containsKey(tu.getId())) {
			res = Response.noContent().build();
		} else {
			res = Response.created(uriInfo.getAbsolutePath()).build();
		}
		TwittStore.instance.getTuP().get(tu.getId()).setDescription(tu.getDescription());
		return res;
	}

	@DELETE
	public void deleteUser() {
		if(!TwittStore.instance.getTuP().containsKey(id))
			throw new NotFoundException("No such TwittUser.");
		TwittStore.instance.getTuP().remove(id);
	}

}
