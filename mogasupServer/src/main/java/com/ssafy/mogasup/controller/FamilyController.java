package com.ssafy.mogasup.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.ssafy.mogasup.dto.Family;
import com.ssafy.mogasup.dto.User;
import com.ssafy.mogasup.model.BasicResponse;
import com.ssafy.mogasup.service.FamilyService;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class FamilyController {
	@Autowired
	FamilyService service;
	
	@PostMapping(value = "/family")
	@ApiOperation(value = "가족 생성", notes = "성공 시 가족 생성")
	
	public Object createFamily(@RequestParam String name, @RequestParam int user_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.createFamily(name);
			int family_id=service.findFamilyId();
			service.createUserFamily(user_id, family_id);
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@PostMapping(value = "/family/user")
	@ApiOperation(value = "가족 구성원 추가", notes = "성공 시 가족 구성원 추가")
	
	public Object addFamily(@RequestParam int family_id, @RequestParam int user_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.createUserFamily(user_id, family_id);
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@GetMapping(value = "/family/{user_id}")
	@ApiOperation(value = "가족 list", notes = "성공 시 가족 list")
	
	public Object familyList(@PathVariable int user_id) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			List<Family> list=service.familyList(user_id);
			result.message = "success";
			result.result=list;
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
}

