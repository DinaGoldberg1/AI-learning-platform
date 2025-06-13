import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface UserState {
  id: string | null;
  name: string | null;
  phoneNumber: string | null;
}

const initialState: UserState = {
  id: null,
  name: null,
  phoneNumber: null,
};

const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {
    setUser(state, action: PayloadAction<{ id: string; name: string; phoneNumber: string }>) {
      state.id = action.payload.id;
      state.name = action.payload.name;
      state.phoneNumber = action.payload.phoneNumber;
    },
    clearUser(state) {
      state.id = null;
      state.name = null;
      state.phoneNumber = null;
    },
  },
});

export const { setUser, clearUser } = userSlice.actions;

export default userSlice.reducer;
