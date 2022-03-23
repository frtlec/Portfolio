import { createReducer, on } from "@ngrx/store";
import { closeRightMenu, openRightMenu } from "./defaultMenu.actions";

export const initialState =false;
const _rightMenu = createReducer(
    initialState,
    on(openRightMenu, (state) =>state=true),
    on(closeRightMenu, (state) =>state=initialState),
  );
   
  export function rightMenuReducer(state:any, action:any) {
    return _rightMenu(state, action);
  }
  