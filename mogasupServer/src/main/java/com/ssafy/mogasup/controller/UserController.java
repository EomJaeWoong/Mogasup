package com.ssafy.mogasup.controller;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import com.ssafy.mogasup.dto.User;
import com.ssafy.mogasup.model.BasicResponse;
import com.ssafy.mogasup.service.UserService;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class UserController {
	@Autowired
	UserService service;
	
	@PostMapping(value = "/user")
	@ApiOperation(value = "회원가입", notes = "성공 시 회원가입 완료")
	public Object insertUser(@RequestParam String email, @RequestParam String nickname, @RequestParam String password) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			User check=service.findByEmail(email);
			
			if(check==null) {
				User user = new User(0,email,nickname,password);
				service.signup(user);
				result.message = "success";
			}
			else {	
				result.message = "exist email";
			}
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@PostMapping(value = "/user/login")
	@ApiOperation(value = "login", notes = "성공 시 login 완료")
	public Object login(@RequestParam String email, @RequestParam String password) {
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			User check=service.login(email, password);
			
			if(check!=null) {
				result.message = "success";
				Map<String, List<String>> map = new HashMap<String, List<String>>();
				User user = service.findByEmail(email);
				List<String> family_id = service.findFamilyByUserId(Integer.toString(user.getUser_id()));
				List<String> user_id = new ArrayList<String>();
				List<String> nickname = new ArrayList<String>();
				user_id.add(Integer.toString(user.getUser_id()));
				map.put("user_id", user_id);
				nickname.add(user.getNickname());
				map.put("nickname", nickname);
				map.put("family_id", family_id);
				result.result = map;
			}
			else {	
				result.message = "no exist email";
			}
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
}
