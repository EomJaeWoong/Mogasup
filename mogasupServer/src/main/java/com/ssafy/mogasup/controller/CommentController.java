package com.ssafy.mogasup.controller;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import com.ssafy.mogasup.model.BasicResponse;
import com.ssafy.mogasup.service.CommentService;
import com.ssafy.mogasup.service.PictureService;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class CommentController {
	@Autowired
	CommentService service;
	
	@PostMapping(value = "/comment")
	@ApiOperation(value = "댓글 생성")
	public Object insertComment(@RequestParam int user_id, @RequestParam int picture_id, @RequestParam("file") MultipartFile file) {
		SimpleDateFormat format= new SimpleDateFormat("yyyyMMddHHmmss");
		Date time = new Date();
		String timeurl=format.format(time);
		
		
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.saveComment(file, timeurl);
//			String voice_path="C:/Users/multicampus/Desktop/picture/"+timeurl+".wav";
			String voice_path="k4a102.p.ssafy.io/backend/voice/"+timeurl+".wav";
			service.insertComment(user_id, picture_id, voice_path);
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@DeleteMapping(value = "/comment/{comment_id}")
	@ApiOperation(value = "댓글 삭제")
	public Object deleteComment(@PathVariable int comment_id) {
		
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.deleteComment(comment_id);
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@GetMapping(value = "/comment/list/{picture_id}")
	@ApiOperation(value = "댓글 list")
	public Object listCommnet(@PathVariable int picture_id) {
		
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			List<HashMap<String, String>> list = service.listComment(picture_id);
			result.result=list;
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
	@GetMapping(value = "/comment/{comment_id}")
	@ApiOperation(value = "댓글 read")
	public Object readComment(@PathVariable int comment_id) {
		
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			String voice_path = service.readComment(comment_id);
			result.result=voice_path;
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
	
}

