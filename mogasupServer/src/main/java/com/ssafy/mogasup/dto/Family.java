package com.ssafy.mogasup.dto;

public class Family {
	private int family_id;
	private String name;
	
	public Family() {
	}

	public Family(int family_id, String name) {
		this.family_id = family_id;
		this.name = name;
	}

	public int getFamily_id() {
		return family_id;
	}

	public void setFamily_id(int family_id) {
		this.family_id = family_id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}
	
	
	
}
