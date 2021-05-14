package com.ssafy.mogasup.controller;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;

import com.ssafy.mogasup.dto.Notice;
import com.ssafy.mogasup.model.BasicResponse;
import com.ssafy.mogasup.service.NoticeService;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class NoticeController {

	@Autowired
	NoticeService service;
	
	@PostMapping(value = "/notice")
	@ApiOperation(value = "공지 추가", notes = "message 성공  success 실패  fail")
	public Object insertNotice(@RequestBody Notice notice) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.insert(notice);
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@GetMapping(value = "/notice/{family_id}")
	@ApiOperation(value = "공지 조회", notes = "message 성공  success 실패  fail")
	public Object getScheduleList(@PathVariable int family_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			List<Notice> list = service.find(family_id);
			List<Map<String, String>> tmp = new ArrayList<Map<String,String>>();
			for (Notice notice : list) {
				Map<String, String> map = new HashMap<String, String>();
				map.put("notice_id", Integer.toString(notice.getNotice_id()));
				map.put("user_id", Integer.toString(notice.getUser_id()));
				map.put("family_id", Integer.toString(notice.getFamily_id()));
				map.put("name", notice.getName());
				map.put("content", notice.getContent());
				map.put("nickname", service.getNickname(notice.getUser_id()));
				map.put("date", notice.getDate());
				tmp.add(map);
			}
			result.result = tmp;
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	
	@DeleteMapping(value = "/notice/{notice_id}")
	@ApiOperation(value = "공지 삭제", notes = "message 성공  success 실패  fail")
	public Object deleteNotice(@PathVariable int notice_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.delete(notice_id);
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@PutMapping(value = "/notice")
	@ApiOperation(value = "공지 수정", notes = "message 성공  success 실패  fail")
	public Object modifySchedule(@RequestBody Notice notice) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.update(notice);
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@GetMapping(value = "/notice/read/{notice_id}")
	@ApiOperation(value = "공지 read", notes = "read 성공  success 실패  fail")
	public Object readnotice(@PathVariable int notice_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			Notice notice=service.read(notice_id);
//			System.out.println(notice);
			String nickname=service.getNickname(notice.getUser_id());
			Map<String, String> map = new HashMap<String, String>();
			map.put("notice_id", Integer.toString(notice.getNotice_id()));
			map.put("user_id", Integer.toString(notice.getUser_id()));
			map.put("family_id", Integer.toString(notice.getFamily_id()));
			map.put("name", notice.getName());
			map.put("content", notice.getContent());
			map.put("nickname", nickname);
			map.put("date", notice.getDate());
			System.out.println(map);
			result.result = map;
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
}
