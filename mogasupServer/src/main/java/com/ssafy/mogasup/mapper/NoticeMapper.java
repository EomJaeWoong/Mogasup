package com.ssafy.mogasup.mapper;

import java.util.List;

import com.ssafy.mogasup.dto.Notice;

public interface NoticeMapper {
	public void insert(Notice notice);
	public void delete(int notice_id);
	public List<Notice> find(int family_id);
	public void update(Notice notice);
	public String getNickname(int user_id);
}
