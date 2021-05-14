package com.ssafy.mogasup.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.ssafy.mogasup.dao.NoticeDao;
import com.ssafy.mogasup.dto.Notice;

@Service
public class NoticeServiceImpl implements NoticeService {

	@Autowired
	NoticeDao dao;
	
	@Override
	public void insert(Notice notice) {
		dao.insert(notice);
	}
	
	@Override
	public void delete(int notice_id) {
		dao.delete(notice_id);
	}
	
	@Override
	public List<Notice> find(int family_id) {
		return dao.find(family_id);
	}
	
	@Override
	public void update(Notice notice) {
		dao.update(notice);
	}
	
	@Override
	public String getNickname(int user_id) {
		return dao.getNickname(user_id);
	}

	@Override
	public Notice read(int notice_id) {
		return dao.read(notice_id);
	}
}
