export interface User {
    key: string;
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    createdAt: Date;
}

export interface Project {
    key: string;
    id: number;
    name: string;
    description: string;
    startDate: Date;
    endDate?: Date;
    status: ProjectStatus;
    risk: ProjectRisk;
}

export enum ProjectStatus {
    UNDER_ANALYSIS = 'under_analysis',
    ANALYSIS_PERFORMED = 'analysis_performed',
    ANALYSIS_APPROVED = 'analysis_approved',
    STARTED = 'started',
    PLANNED = 'planned',
    IN_PROGRESS = 'in_progress',
    COMPLETED = 'completed',
    CANCELLED = 'cancelled'
}

export enum ProjectStatusTitle {
    UNDER_ANALYSIS = 'Em Análise',
    ANALYSIS_PERFORMED = 'Análise Realizada',
    ANALYSIS_APPROVED = 'Análise Aprovada',
    STARTED = 'Iniciado',
    PLANNED = 'Planejado',
    IN_PROGRESS = 'Em Progresso',
    COMPLETED = 'Concluído',
    CANCELLED = 'Cancelado'
}

export interface EnumType {
    key: ProjectStatus;
    value: ProjectStatusTitle;
}

export const enumToEnumTypeArray = (enumObject: any, enumTitleObject: any): EnumType[] => {
    return Object.entries(enumObject).map(([key, value]) => ({
        key: value as ProjectStatus,
        value: enumTitleObject[key] as ProjectStatusTitle
    }));
};



export enum ProjectRisk {
    LOW_RISK = 'low_risk',
    MEDIUM_RISK = 'medium_risk',
    HIGH_RISK = 'high_risk'
}

export enum ProjectRiskTitle {
    LOW_RISK = 'Baixo Risco',
    MEDIUM_RISK = 'Médio Risco',
    HIGH_RISK = 'Alto Risco'
}

export interface ProjectDTO {
    key: string;
    id: number;
    name: string;
    description: string;
    startDate: Date;
    endDate?: Date;
    status: string;
    risk: string;
    responmsable: User;
    users: User[];
}

export interface ProjectUser {
    key: string;
    id: number;
    projectId: number;
    userId: number;
}

export interface ListProjectsProps {
    OpenProjectDetail: (type: string, key: React.Key) => void;
}

export interface CreateUserProps {
    handleCreateUser: (value: User) => void;
}
  
export interface AtlasState {
    [x: string]: any;
    users: User[] | null;
    projects: Project[] | null;
}