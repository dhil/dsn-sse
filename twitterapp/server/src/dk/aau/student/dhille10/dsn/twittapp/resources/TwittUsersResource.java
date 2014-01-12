package dk.aau.student.dhille10.dsn.twittapp.resources;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.ws.rs.Consumes;
import javax.ws.rs.DELETE;
import javax.ws.rs.GET;
import javax.ws.rs.POST;
import javax.ws.rs.Path;
import javax.ws.rs.PathParam;
import javax.ws.rs.Produces;
import javax.ws.rs.core.Context;
import javax.ws.rs.core.MediaType;
import javax.ws.rs.core.Request;
import javax.ws.rs.core.Response;
import javax.ws.rs.core.UriInfo;
import javax.xml.bind.JAXBElement;

import com.sun.jersey.api.NotFoundException;

import dk.aau.student.dhille10.dsn.twittapp.models.*;
import dk.aau.student.dhille10.dsn.twittapp.storage.*;

/*Resource for TwittUsers*/
/*See TwittStatuses*/

@Path("/twittusers")
public class TwittUsersResource {
	@Context
	UriInfo uriInfo;
	@Context
	Request request;

	@GET
	@Produces({MediaType.APPLICATION_XML})
	public List<TwittUser> getUsers() {
		List<TwittUser> tus = new ArrayList<TwittUser>();
		tus.addAll(TwitterStore.instance.getTuP().values());
		return tus; 

	}
	
	@GET
	@Path("{id}")
	@Produces({MediaType.APPLICATION_XML})
	public TwittUser getUser(@PathParam("id") String id) {
		if (TwitterStore.instance.getTuP().containsKey(id))
			return TwitterStore.instance.getTuP().get(id);
		else
			throw new NotFoundException();
	}

	@GET
	@Path("count")
	@Produces(MediaType.TEXT_PLAIN)
	public String getTuCount() {
		int count = TwitterStore.instance.getTuP().size();
		return String.valueOf(count);
	}

	@POST
	@Consumes(MediaType.APPLICATION_XML)
	public Response newTu(JAXBElement<TwittUser> jaxbMessage) 
			throws IOException {
		
		TwittUser tu = jaxbMessage.getValue();
		Boolean exists = TwitterStore.instance.getTuP().containsKey(tu.getId());
		if (exists)
			return Response.status(202).build();
		
		TwitterStore.instance.getTuP().put(tu.getId(), tu);
		
		return Response.status(201).build();
	}

	@Path("{id}")
	public TwittUserResource getMessage(
			@PathParam("id") String id) {
		return new TwittUserResource(uriInfo, request, id);
	}
	
	@DELETE
	@Path("{id}")
	public Response deleteTu(@PathParam("id") String id) {
		if (TwitterStore.instance.getTuP().containsKey(id))
			TwitterStore.instance.getTuP().remove(id);
		
		return Response.status(200).build();
	}
}
