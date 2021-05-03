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

import com.ssafy.mogasup.dto.Schedule;
import com.ssafy.mogasup.model.BasicResponse;
import com.ssafy.mogasup.service.ScheduleService;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class ScheduleController {

	@Autowired
	ScheduleService service;
	
	@PostMapping(value = "/schedule")
	@ApiOperation(value = "스케줄 추가", notes = "message 성공  success 실패  fail")
	public Object insertSchedule(@RequestBody Schedule schedule) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.insert(schedule);
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@GetMapping(value = "/schedule/{family_id}")
	@ApiOperation(value = "스케줄 조회", notes = "message 성공  success 실패  fail")
	public Object getScheduleList(@PathVariable int family_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			List<Schedule> list = service.find(family_id);
			List<Map<String, String>> tmp = new ArrayList<Map<String,String>>();
			for (Schedule schedule : list) {
				Map<String, String> map = new HashMap<String, String>();
				map.put("schedule_id", Integer.toString(schedule.getSchdule_id()));
				map.put("user_id", Integer.toString(schedule.getUser_id()));
				map.put("family_id", Integer.toString(schedule.getFamily_id()));
				map.put("name", schedule.getName());
				map.put("content", schedule.getContent());
				map.put("nickname", service.getNickname(schedule.getUser_id()));
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
	
	
	@DeleteMapping(value = "/schedule/{schedule_id}")
	@ApiOperation(value = "스케줄 삭제", notes = "message 성공  success 실패  fail")
	public Object deleteSchedule(@PathVariable int schedule_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.delete(schedule_id);
			result.message = "success";
		}catch(Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			result.message = "fail";
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@PutMapping(value = "/schedule")
	@ApiOperation(value = "스케줄 수정", notes = "message 성공  success 실패  fail")
	public Object modifySchedule(@RequestBody Schedule schedule) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.update(schedule);
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
