//initialization
@256
D=A
@SP
M=D
@300
D=A
@LCL
M=D
@400
D=A
@ARG
M=D
@3000
D=A
@THIS
M=D
@3010
D=A
@THAT
M=D
@5
D=A
@TMP
M=D
//initialization end
//start CPush constant 10
@10
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 10
//start CPop local 0
//Local0
@LCL
D=M
@0
D=D+A
@R13
M=D
@SP
M=M-1
@SP
A=M
D=M
@R13
A=M
M=D
//endLocal0
//end CPop local 0
