package com.ssafy.mogasup.controller;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import com.ssafy.mogasup.model.BasicResponse;
import com.ssafy.mogasup.service.PictureService;

import io.swagger.annotations.ApiOperation;

@CrossOrigin
@RestController
public class PictureController {
	@Autowired
	PictureService service;
	
	@PostMapping(value = "/picture")
	@ApiOperation(value = "사진 생성")
	public Object insertImage(@RequestParam int family_id, @RequestParam("file") MultipartFile file) {
		SimpleDateFormat format= new SimpleDateFormat("yyyyMMddHHmmss");
		Date time = new Date();
		String timeurl=format.format(time);
		
		
		BasicResponse result = new BasicResponse();
		HttpStatus status;
		try {
			service.saveimage(file,timeurl);
			String image_path="C:/Users/multicampus/Desktop/picture/"+timeurl+file.getOriginalFilename();
			service.insertImage(family_id, image_path);
			result.message = "success";
		} catch (Exception e) {
			status=HttpStatus.INTERNAL_SERVER_ERROR;
			e.printStackTrace();
		}
		status= HttpStatus.ACCEPTED;
        return new ResponseEntity<>(result, status);
	}
}

