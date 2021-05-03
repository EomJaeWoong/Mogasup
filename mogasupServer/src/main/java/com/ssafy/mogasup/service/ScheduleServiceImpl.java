package com.ssafy.mogasup.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ssafy.mogasup.dao.ScheduleDao;
import com.ssafy.mogasup.dto.Schedule;

@Service
public class ScheduleServiceImpl implements ScheduleService {
	
	@Autowired
	ScheduleDao dao;
	
	@Override
	public void insert(Schedule schedule) {
		dao.insert(schedule);
	}
	
	@Override
	public void delete(int schedule_id) {
		dao.delete(schedule_id);
	}
	
	@Override
	public List<Schedule> find(int family_id) {
		return dao.find(family_id);
	}
	
	@Override
	public void update(Schedule schedule) {
		dao.update(schedule);
	}
	
	@Override
	public String getNickname(int user_id) {
		return dao.getNickname(user_id);
	}
}
