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
//start CPush constant 3030
@3030
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 3030
//start CPop pointer 0
//Pointer0
@THIS
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
//endPointer0
//end CPop pointer 0
//start CPush constant 3040
@3040
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 3040
//start CPop pointer 1
//Pointer1
@THAT
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
//endPointer1
//end CPop pointer 1
//start CPush constant 32
@32
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 32
//start CPop this 2
//This2
@THIS
D=M
@2
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
//endThis2
//end CPop this 2
//start CPush constant 46
@46
D=A
@SP
A=M
M=D
@SP
M=M+1
//end CPush constant 46
//start CPop that 6
//That6
@THAT
D=M
@6
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
//endThat6
//end CPop that 6
//start CPush pointer 0
@THIS
D=M
@SP
A=M
M=D
@SP
M=M+1
//end CPush pointer 0
//start CPush pointer 1
@THAT
D=M
@SP
A=M
M=D
@SP
M=M+1
//end CPush pointer 1
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
//start CPush this 2
@THIS
D=M
@2
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end CPush this 2
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
//start CPush that 6
@THAT
D=M
@6
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//end CPush that 6
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
