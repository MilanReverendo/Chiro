export interface UserShortDto {
  id: string;
  username: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  phoneNumber?: string;
  dateOfBirth?: string;
  isGroupLeader: boolean;
  profileImageUrl?: string;
  groupId?: string;
}
