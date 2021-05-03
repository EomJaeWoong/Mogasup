package com.ssafy.mogasup.dao;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.ssafy.mogasup.dto.Schedule;
import com.ssafy.mogasup.mapper.ScheduleMapper;

@Repository
public class ScheduleDaoImpl implements ScheduleDao {
	
	@Autowired
	ScheduleMapper mapper;
	
	@Override
	public void insert(Schedule schedule) {
		mapper.insert(schedule);
	}
	
	@Override
	public void delete(int schedule_id) {
		mapper.delete(schedule_id);
	}
	
	@Override
	public List<Schedule> find(int family_id) {
		return mapper.find(family_id);
	}
	
	@Override
	public void update(Schedule schedule) {
		mapper.update(schedule);
	}
	
	@Override
	public String getNickname(int user_id) {
		return mapper.getNickname(user_id);
	}
}
