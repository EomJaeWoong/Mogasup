package com.ssafy.mogasup.dto;

public class Schedule {
	private int schedule_id;
	private int family_id;
	private int user_id;
	private String name;
	private String content;
	private String date;
	
	public Schedule() {
	}

	public int getSchedule_id() {
		return schedule_id;
	}

	public void setSchedule_id(int schdule_id) {
		this.schedule_id = schdule_id;
	}

	public int getFamily_id() {
		return family_id;
	}

	public void setFamily_id(int family_id) {
		this.family_id = family_id;
	}

	public int getUser_id() {
		return user_id;
	}

	public void setUser_id(int user_id) {
		this.user_id = user_id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	public String getDate() {
		return date;
	}

	public void setDate(String date) {
		this.date = date;
	}
	
}
