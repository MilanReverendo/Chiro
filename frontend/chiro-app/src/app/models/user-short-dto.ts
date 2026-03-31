export interface UserShortDto {
  id: string;
  username: string;
  firstName?: string;
  lastName?: string;
  isGroupLeader: boolean;
  groupId?: string;
}
