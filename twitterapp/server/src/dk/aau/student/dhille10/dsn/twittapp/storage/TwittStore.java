package dk.aau.student.dhille10.dsn.twittapp.storage;

import java.util.HashMap;
import java.util.Map;

import dk.aau.student.dhille10.dsn.twittapp.models.*;

/*This singleton is used as data provider
 * In a real world case a database should be used, 
 * but it is enough for this exercise.
 */

public enum TwittStore {
	instance;

	/*HashMap to store the TwittUsers*/
	private Map<String, TwittUser> tuProvider = new HashMap<String, TwittUser>();
	
	/*Constructor, we create one user at the beginning to have something to access*/
	private TwittStore(){
		TwittUser tu = new TwittUser("1", "Admin", "Admin", "AAU", "The Admin");
		tuProvider.put(tu.getId(), tu);
	}

	/*Getters*/
	
	public Map<String, TwittUser> getTuP() {
		return tuProvider;
	}


}
