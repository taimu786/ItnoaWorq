// src/features/profile/profileSlice.ts

import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import {
  getMyProfileApi,
  updateProfileApi,
  updateSkillsApi,
  addEducationApi,
  addExperienceApi,
} from "./profileService";
import type { ProfileDto, SkillDto, EducationDto, ExperienceDto } from "../../types/profile";

interface ProfileState {
  profile: ProfileDto | null;
  loading: boolean;
}

const initialState: ProfileState = {
  profile: null,
  loading: false,
};

// 🔹 FETCH
export const fetchProfile = createAsyncThunk(
  "profile/fetch",
  async () => {
    return await getMyProfileApi();
  }
);

// 🔹 UPDATE PROFILE
export const updateProfile = createAsyncThunk(
  "profile/update",
  async (data: ProfileDto, { dispatch }) => {
    await updateProfileApi(data);
    dispatch(fetchProfile());
  }
);

// 🔹 UPDATE SKILLS
export const updateSkills = createAsyncThunk(
  "profile/updateSkills",
  async (skills: SkillDto[], { dispatch }) => {
    await updateSkillsApi(skills);
    dispatch(fetchProfile());
  }
);

// 🔹 ADD EDUCATION
export const addEducation = createAsyncThunk(
  "profile/addEducation",
  async (dto: EducationDto, { dispatch }) => {
    await addEducationApi(dto);
    dispatch(fetchProfile());
  }
);

// 🔹 ADD EXPERIENCE
export const addExperience = createAsyncThunk(
  "profile/addExperience",
  async (dto: ExperienceDto, { dispatch }) => {
    await addExperienceApi(dto);
    dispatch(fetchProfile());
  }
);

const profileSlice = createSlice({
  name: "profile",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProfile.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchProfile.fulfilled, (state, action) => {
        state.loading = false;
        state.profile = action.payload;
      })
      .addCase(fetchProfile.rejected, (state) => {
        state.loading = false;
      });
  },
});

export default profileSlice.reducer;