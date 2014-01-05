package dk.aau.student.dhille10.dsn.twittapp.resources;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.ws.rs.Consumes;
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
		tus.addAll(TwittStore.instance.getTuP().values());
		return tus; 

	}

	@GET
	@Path("count")
	@Produces(MediaType.TEXT_PLAIN)
	public String getTuCount() {
		int count = TwittStore.instance.getTuP().size();
		return String.valueOf(count);
	}

	@POST
	@Consumes(MediaType.APPLICATION_XML)
	public Response newTu(JAXBElement<TwittUser> jaxbMessage) 
			throws IOException {
		TwittUser tu = jaxbMessage.getValue();
		TwittStore.instance.getTuP().put(tu.getId(), tu);
		return Response.status(201).build();
	}

	@Path("{id}")
	public TwittUserResource getMessage(
			@PathParam("id") String id) {
		return new TwittUserResource(uriInfo, request, id);
	}
}
