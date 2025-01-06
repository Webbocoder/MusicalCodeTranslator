Musical Code Translator

I am 42% of the way through a Udemy course in C# and I thought I'd try creating something with what I have learnt so far. The premise of this is to simply take each letter of a string and convert it to its equivalent in the musical alphabet (A-G) together with a number to represent which segment of seven letters of the English alphabet the now-musical letter belongs.

A = A0
B = B0
C = C0
D = D0
E = E0
F = F0
G = G0
H = A1
I = B1
J = C1
K = D1
L = E1
M = F1
N = G1
O = A2
P = B2
Q = C2
R = D2
S = E2
T = F2
U = G2
V = A3
W = B3
X = C3
Y = D3
Z = E3

There is also a playback option (currently for Windows only) which dynamically converts each of the above into frequencies, then calculates the duration of each note based on a tempo and word-length such that each word lasts one beat (a one-letter will last one beat; each letter in a two-letter word will last half a beat each; each letter in a three-letter word will last a third of a beat each; etc).

My solution is not perfect as it stands; I was going to incorporate punctuation and turn them into rests. Also, the playback uses Console.Beep which is only availible on Windows. Plus it might be nice to have a GUI / be able to put it onto a phone so I could message people with my code. I will try to continue improving this project, though I have decided to start job hunting again in 2025.
