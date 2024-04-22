import { createStore } from "redux";
import { combineReducers } from "redux";
import atlasReducer from "./atlasReducer";

const rootReducer = combineReducers({
    atlas: atlasReducer
});

const store = createStore(rootReducer);

export default store;
