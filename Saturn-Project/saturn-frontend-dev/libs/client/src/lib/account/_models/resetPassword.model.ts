export class ResetPasswordDto {
    userId : string | undefined;

    password: string | undefined;
    
    resetToken : string | undefined;
}