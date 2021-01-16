export interface Result<T> {
  value: T;
  success: boolean;
  errors: string[];
}

export interface GroupItem {
  groupId: string;
  title: string;
}

export interface FindSalespersonCriteria {
  language: string;
  speciality: string;
}

export enum Language {
  Any = 1,
  SpeakingGreek = 2,
  NotSpeakingGreek = 3
}
