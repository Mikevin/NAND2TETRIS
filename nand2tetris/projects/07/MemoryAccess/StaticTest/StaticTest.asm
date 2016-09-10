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
//start Push constant 111
@111
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 111
//start Push constant 333
@333
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 333
//start Push constant 888
@888
D=A
@SP
A=M
M=D
@SP
M=M+1
//end Push constant 888
//start Pop static 8
//Static8
@StaticTest.8
D=A
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
//endStatic8
//end Pop static 8
//start Pop static 3
//Static3
@StaticTest.3
D=A
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
//endStatic3
//end Pop static 3
//start Pop static 1
//Static1
@StaticTest.1
D=A
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
//endStatic1
//end Pop static 1
//start Push static 3
@StaticTest.3
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push static 3
//start Push static 1
@StaticTest.1
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push static 1
//sub
@SP
M=M-1
@SP
A=M
D=M
@SP
M=M-1
@SP
A=M
D=M-D
@SP
A=M
M=D
@SP
M=M+1
//sub end
//start Push static 8
@StaticTest.8
D=M
@SP
A=M
M=D
@SP
M=M+1
//end Push static 8
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
