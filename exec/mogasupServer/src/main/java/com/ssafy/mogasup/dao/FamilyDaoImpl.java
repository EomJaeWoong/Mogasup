package com.ssafy.mogasup.dao;

import java.util.HashMap;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Repository;

import com.ssafy.mogasup.dto.Family;
import com.ssafy.mogasup.mapper.FamilyMapper;

@Repository
public class FamilyDaoImpl implements FamilyDao{
	@Autowired
	FamilyMapper mapper;
	
	@Override
	public void createFamily(String name) {
		mapper.createFamily(name);
	}

	@Override
	public int findFamilyId() {
		return mapper.findFamilyId();
	}

	@Override
	public void createUserFamily(int user_id, int family_id) {
		mapper.createUserFamily(user_id, family_id);
	}

	@Override
	public List<Family> familyList(int user_id) {
		return mapper.familyList(user_id);
	}

	@Override
	public List<HashMap<String, String>> familyMemberList(int family_id) {
		return mapper.familyMemberList(family_id);
	}

	@Override
	public void deleteFamily(int family_id) {
		mapper.deleteFamily(family_id);
	}

	@Override
	public void deleteFamilyMember(int user_id, int family_id) {
		mapper.deleteFamilyMember(user_id, family_id);
	}

	@Override
	public String familyName(int family_id) {
		return mapper.familyName(family_id);
	}

		
}
