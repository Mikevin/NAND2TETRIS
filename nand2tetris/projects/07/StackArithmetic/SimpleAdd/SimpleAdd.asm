//initialization
@256
D=A
@SP
M=D
//initialization end
//start CPush constant 7
@7
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 7
//start CPush constant 8
@8
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 8
//add
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=D+M
@SP
A=M
M=D
@SP
M=M+1
//add end
