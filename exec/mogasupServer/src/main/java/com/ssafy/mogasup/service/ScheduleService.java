package com.ssafy.mogasup.service;

import java.util.List;

import com.ssafy.mogasup.dto.Schedule;

public interface ScheduleService {
	public void insert(Schedule schedule);
	public void delete(int schedule_id);
	public List<Schedule> find(int family_id);
	public void update(Schedule schedule);
	public String getNickname(int user_id);
}
