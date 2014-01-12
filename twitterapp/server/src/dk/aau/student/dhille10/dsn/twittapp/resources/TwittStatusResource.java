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

public class TwittStatusResource {

	@Context
	UriInfo uriInfo;
	@Context
	Request request;
	String id;

	public TwittStatusResource(UriInfo uriInfo, Request request, String id) {
		this.uriInfo = uriInfo;
		this.request = request;
		this.id = id;
	}

	@GET
	@Produces(MediaType.APPLICATION_XML)
	public TwittStatus getStatus() {
		TwittStatus tu = TwitterStore.instance.getStP().get(id);
		if(tu==null)
			throw new NotFoundException("No such TwittStatus.");
		return tu;
	}

	@PUT
	@Consumes(MediaType.APPLICATION_XML)
	public Response putStatusText(JAXBElement<TwittStatus> jaxbMessage) {
		TwittStatus st = jaxbMessage.getValue();
		return putStatus(st);
	}


	private Response putStatus(TwittStatus st) {
		Response res;
		if(!st.getId().equals(id))
			st.setId(id);
		if(TwitterStore.instance.getStP().containsKey(st.getId())) {
			res = Response.noContent().build();
		} else {
			res = Response.created(uriInfo.getAbsolutePath()).build();
		}
		TwitterStore.instance.getStP().get(st.getId()).setText(st.getText());
		return res;
	}

	@DELETE
	public void deleteStatus() {
		if(!TwitterStore.instance.getTuP().containsKey(id))
			throw new NotFoundException("No such TwittStatus.");
		TwitterStore.instance.getStP().remove(id);
	}
}
