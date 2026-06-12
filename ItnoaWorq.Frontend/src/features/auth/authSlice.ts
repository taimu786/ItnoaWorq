import { createSlice } from "@reduxjs/toolkit";
import type { User } from "../../types/auth";


type AuthState = {
  user: User | null;
  token: string | null;
  loading: boolean;
};

const initialState: AuthState = {
  user: JSON.parse(localStorage.getItem("user") || "null"),
  token: localStorage.getItem("token"),
  loading: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setAuth: (state, action) => {
      state.user = action.payload.user;
      state.token = action.payload.token;

      localStorage.setItem("user", JSON.stringify(action.payload.user));
      localStorage.setItem("token", action.payload.token);
    },
    logout: (state) => {
      state.user = null;
      state.token = null;

      localStorage.clear();
    },
  },
});

export const { setAuth, logout } = authSlice.actions;
export default authSlice.reducer;