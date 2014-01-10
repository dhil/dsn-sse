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

@Path("/twittstatuses")
public class TwittStatusesResource {
	@Context
	UriInfo uriInfo;
	@Context
	Request request;

	@GET
	@Produces({MediaType.APPLICATION_XML})
	public List<TwittStatus> getStatuses() {
		List<TwittStatus> sts = new ArrayList<TwittStatus>();
		sts.addAll(TwitterStore.instance.getStP().values());
		return sts; 

	}
	
	@GET
	@Produces({MediaType.APPLICATION_XML})
	@Path("/user/{userid}")
	public List<TwittStatus> getStatusesByUser(@PathParam("userid") String userid) {
		List<TwittStatus> sts = new ArrayList<TwittStatus>();
		for (TwittStatus s : TwitterStore.instance.getStP().values()) {
			if (s.getUserId().compareTo(userid) == 0)
				sts.add(s);
		}
		return sts;
	}

	@GET
	@Path("count")
	@Produces(MediaType.TEXT_PLAIN)
	public String getStCount() {
		int count = TwitterStore.instance.getStP().size();
		return String.valueOf(count);
	}

	@POST
	@Consumes(MediaType.APPLICATION_XML)
	public Response newSt(JAXBElement<TwittStatus> jaxbMessage) 
			throws IOException {
		
		TwittStatus st = jaxbMessage.getValue();
		TwitterStore.instance.getStP().put(st.getId(), st);
		
		return Response.status(201).build();
	}

	@Path("{id}")
	public TwittStatusResource getMessage(
			@PathParam("id") String id) {
		return new TwittStatusResource(uriInfo, request, id);
	}
}
