import { UserShortDto } from './user-short-dto';

export interface GroupDto {
  id?: string;
  name: string;
  description?: string;
  leaders?: UserShortDto[];
}
