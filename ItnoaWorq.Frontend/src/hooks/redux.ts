import { useDispatch, useSelector } from "react-redux";
import type { TypedUseSelectorHook } from "react-redux";
import type { RootState, AppDispatch } from "../app/store";

// ✅ Typed dispatch
export const useAppDispatch = () => useDispatch<AppDispatch>();

// ✅ Typed selector
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;