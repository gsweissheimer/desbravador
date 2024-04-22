import { User, Project, AtlasState } from "../types";
import { AtlasAction } from "../actions/atlasActions";

const initialState: AtlasState = {
    users: null,
    projects: null
};

const atlasReducer = (state = initialState, action: AtlasAction): AtlasState => {
    switch (action.type) {
        case "SET_USER":
            return { ...state, users: action.payload, projects: state.projects };
        case "SET_PROJECT":
            return { ...state, users: state.users, projects: action.payload };
        default:
            return state;
    }
};

export default atlasReducer;
