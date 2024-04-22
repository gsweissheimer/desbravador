import { User, Project } from "../types";

export const setUser = (users: User[]) => {
    return {
        type: "SET_USER",
        payload: users
    } as const;
};

export const setProject = (projects: Project[]) => {
    return {
        type: "SET_PROJECT",
        payload: projects
    } as const;
};

export const fetchUserFromAPI = async (): Promise<User[]> => {
    try {
        const response = await fetch("http://localhost:5130/api/User"); // Substitua pela URL da sua API
        if (!response.ok) {
            throw new Error("Erro ao buscar dados do usuário");
        }
        const data = await response.json();
        return data as User[];
    } catch (error) {
        throw new Error(`Erro ao buscar dados do usuário`);
    }
};

export const fetchProjectFromAPI = async (): Promise<Project[]> => {
    try {
        const response = await fetch("http://localhost:5130/api/Project"); // Substitua pela URL da sua API
        if (!response.ok) {
            throw new Error("Erro ao buscar dados do projeto");
        }
        const data = await response.json();
        return data as Project[];
    } catch (error) {
        throw new Error(`Erro ao buscar dados do projeto`);
    }
};

export type AtlasAction = ReturnType<typeof setUser> | ReturnType<typeof setProject>;
