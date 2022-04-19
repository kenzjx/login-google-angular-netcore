export interface UserToken {
  userId :string,
  expires: any,
  email: string,
  token: string
}

export class userT implements UserToken
{
  userId = '';
  expires = Date.now();
  email ='';
  token = '';

}