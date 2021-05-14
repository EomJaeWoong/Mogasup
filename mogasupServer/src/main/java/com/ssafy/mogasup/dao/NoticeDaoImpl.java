package com.ssafy.mogasup.dao;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.ssafy.mogasup.dto.Notice;
import com.ssafy.mogasup.mapper.NoticeMapper;

@Repository
public class NoticeDaoImpl implements NoticeDao {
	
	@Autowired
	NoticeMapper mapper;
	
	@Override
	public void insert(Notice notice) {
		mapper.insert(notice);
	}
	
	@Override
	public void delete(int notice_id) {
		mapper.delete(notice_id);
	}
	
	@Override
	public List<Notice> find(int family_id) {
		return mapper.find(family_id);
	}
	
	@Override
	public void update(Notice notice) {
		mapper.update(notice);
	}
	
	@Override
	public String getNickname(int user_id) {
		return mapper.getNickname(user_id);
	}

	@Override
	public Notice read(int notice_id) {
		return mapper.read(notice_id);
	}
}
