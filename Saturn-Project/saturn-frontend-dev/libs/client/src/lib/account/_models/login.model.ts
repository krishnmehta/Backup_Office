export class LoginModel{
    userNameOrEmailAddress : string | undefined;
    password : string | undefined;
    rememberMe  = true;
}

export class LoginResponse{
    result!: number;
    description: string | undefined;
}