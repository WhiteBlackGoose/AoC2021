# Codegen analysis report
*2021-12-03 06:43 UTC*
## Summary

| Job             | Method              | Branches  | Calls  | StaticStackAllocations  | CodegenSize  | ILSize  |
|:---------------:|:-------------------:|:---------:|:------:|:-----------------------:|:------------:|:-------:|
| (Tier = Tier1)  | Int32 PartI()       | 22        | 1      | 88 B                    | 631 B        | 69 B    |
| (Tier = Tier1)  | Int32 PartII()      | 22        | 1      | 88 B                    | 642 B        | 69 B    |
| (Tier = Tier1)  | Int32 PartI_ptr()   | 15        |  -     | 40 B                    | 317 B        | 100 B   |
| (Tier = Tier1)  | Int32 PartII_ptr()  | 15        |  -     | 40 B                    | 347 B        | 100 B   |

## Codegens

### Int32 PartI(): Tier1
```assembly
00007FFBAB3BD280 4883EC58             sub       rsp,58h
00007FFBAB3BD284 C5F877               vzeroupper
00007FFBAB3BD287 C5D857E4             vxorps    xmm4,xmm4,xmm4
00007FFBAB3BD28B C5F97F642440         vmovdqa   [rsp+40h],xmm4
00007FFBAB3BD291 33C0                 xor       eax,eax
00007FFBAB3BD293 4889442450           mov       [rsp+50h],rax
00007FFBAB3BD298 488B4108             mov       rax,[rcx+8]
00007FFBAB3BD29C 4885C0               test      rax,rax
00007FFBAB3BD29F 7506                 jne       short 0000`7FFB`AB3B`D2A7h
00007FFBAB3BD2A1 33D2                 xor       edx,edx
00007FFBAB3BD2A3 33C9                 xor       ecx,ecx
00007FFBAB3BD2A5 EB07                 jmp       short 0000`7FFB`AB3B`D2AEh
00007FFBAB3BD2A7 488D500C             lea       rdx,[rax+0Ch]
00007FFBAB3BD2AB 8B4808               mov       ecx,[rax+8]
00007FFBAB3BD2AE 488D442440           lea       rax,[rsp+40h]
00007FFBAB3BD2B3 488910               mov       [rax],rdx
00007FFBAB3BD2B6 894808               mov       [rax+8],ecx
00007FFBAB3BD2B9 33C0                 xor       eax,eax
00007FFBAB3BD2BB 89442450             mov       [rsp+50h],eax
00007FFBAB3BD2BF 33D2                 xor       edx,edx
00007FFBAB3BD2C1 33C9                 xor       ecx,ecx
00007FFBAB3BD2C3 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD2C8 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3BD2CD 0F8D16020000         jge       near 0000`7FFB`AB3B`D4E9h
00007FFBAB3BD2D3 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD2D8 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD2DD 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD2E1 0F830A020000         jae       near 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD2E7 4D8B09               mov       r9,[r9]
00007FFBAB3BD2EA 4D63C0               movsxd    r8,r8d
00007FFBAB3BD2ED 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD2F2 4183F864             cmp       r8d,64h
00007FFBAB3BD2F6 0F84E6000000         je        near 0000`7FFB`AB3B`D3E2h
00007FFBAB3BD2FC 4183F866             cmp       r8d,66h
00007FFBAB3BD300 7450                 je        short 0000`7FFB`AB3B`D352h
00007FFBAB3BD302 4183F875             cmp       r8d,75h
00007FFBAB3BD306 0F859F010000         jne       near 0000`7FFB`AB3B`D4ABh
00007FFBAB3BD30C 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD311 4183C003             add       r8d,3
00007FFBAB3BD315 4489442450           mov       [rsp+50h],r8d
00007FFBAB3BD31A 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD31F 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD324 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD328 0F83C3010000         jae       near 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD32E 4D8B09               mov       r9,[r9]
00007FFBAB3BD331 4D63C0               movsxd    r8,r8d
00007FFBAB3BD334 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD339 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3BD33D 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3BD342 41FFC1               inc       r9d
00007FFBAB3BD345 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3BD34A 412BD0               sub       edx,r8d
00007FFBAB3BD34D E926010000           jmp       0000`7FFB`AB3B`D478h
00007FFBAB3BD352 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD357 4183C008             add       r8d,8
00007FFBAB3BD35B 4489442450           mov       [rsp+50h],r8d
00007FFBAB3BD360 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD365 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD36A 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD36E 0F837D010000         jae       near 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD374 4D8B09               mov       r9,[r9]
00007FFBAB3BD377 4D63C0               movsxd    r8,r8d
00007FFBAB3BD37A 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD37F 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3BD383 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3BD388 41FFC1               inc       r9d
00007FFBAB3BD38B 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3BD390 4103C0               add       eax,r8d
00007FFBAB3BD393 EB0D                 jmp       short 0000`7FFB`AB3B`D3A2h
00007FFBAB3BD395 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD39A 41FFC0               inc       r8d
00007FFBAB3BD39D 4489442450           mov       [rsp+50h],r8d
00007FFBAB3BD3A2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD3A7 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3BD3AC 0F8DF9000000         jge       near 0000`7FFB`AB3B`D4ABh
00007FFBAB3BD3B2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD3B7 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD3BC 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD3C0 0F832B010000         jae       near 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD3C6 4D8B09               mov       r9,[r9]
00007FFBAB3BD3C9 4D63C0               movsxd    r8,r8d
00007FFBAB3BD3CC 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD3D1 4183F861             cmp       r8d,61h
00007FFBAB3BD3D5 7CBE                 jl        short 0000`7FFB`AB3B`D395h
00007FFBAB3BD3D7 4183F87A             cmp       r8d,7Ah
00007FFBAB3BD3DB 7FB8                 jg        short 0000`7FFB`AB3B`D395h
00007FFBAB3BD3DD E9C9000000           jmp       0000`7FFB`AB3B`D4ABh
00007FFBAB3BD3E2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD3E7 4183C005             add       r8d,5
00007FFBAB3BD3EB 4489442450           mov       [rsp+50h],r8d
00007FFBAB3BD3F0 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD3F5 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD3FA 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD3FE 0F83ED000000         jae       near 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD404 4D8B09               mov       r9,[r9]
00007FFBAB3BD407 4D63C0               movsxd    r8,r8d
00007FFBAB3BD40A 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD40F 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3BD413 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3BD418 41FFC1               inc       r9d
00007FFBAB3BD41B 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3BD420 4103D0               add       edx,r8d
00007FFBAB3BD423 EB0D                 jmp       short 0000`7FFB`AB3B`D432h
00007FFBAB3BD425 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD42A 41FFC0               inc       r8d
00007FFBAB3BD42D 4489442450           mov       [rsp+50h],r8d
00007FFBAB3BD432 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD437 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3BD43C 7D6D                 jge       short 0000`7FFB`AB3B`D4ABh
00007FFBAB3BD43E 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD443 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD448 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD44C 0F839F000000         jae       near 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD452 4D8B09               mov       r9,[r9]
00007FFBAB3BD455 4D63C0               movsxd    r8,r8d
00007FFBAB3BD458 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD45D 4183F861             cmp       r8d,61h
00007FFBAB3BD461 7CC2                 jl        short 0000`7FFB`AB3B`D425h
00007FFBAB3BD463 4183F87A             cmp       r8d,7Ah
00007FFBAB3BD467 7FBC                 jg        short 0000`7FFB`AB3B`D425h
00007FFBAB3BD469 EB40                 jmp       short 0000`7FFB`AB3B`D4ABh
00007FFBAB3BD46B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD470 41FFC0               inc       r8d
00007FFBAB3BD473 4489442450           mov       [rsp+50h],r8d
00007FFBAB3BD478 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD47D 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3BD482 7D27                 jge       short 0000`7FFB`AB3B`D4ABh
00007FFBAB3BD484 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD489 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3BD48E 453B4108             cmp       r8d,[r9+8]
00007FFBAB3BD492 735D                 jae       short 0000`7FFB`AB3B`D4F1h
00007FFBAB3BD494 4D8B09               mov       r9,[r9]
00007FFBAB3BD497 4D63C0               movsxd    r8,r8d
00007FFBAB3BD49A 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3BD49F 4183F861             cmp       r8d,61h
00007FFBAB3BD4A3 7CC6                 jl        short 0000`7FFB`AB3B`D46Bh
00007FFBAB3BD4A5 4183F87A             cmp       r8d,7Ah
00007FFBAB3BD4A9 7FC0                 jg        short 0000`7FFB`AB3B`D46Bh
00007FFBAB3BD4AB C5F857C0             vxorps    xmm0,xmm0,xmm0
00007FFBAB3BD4AF C5F911442420         vmovupd   [rsp+20h],xmm0
00007FFBAB3BD4B5 89442420             mov       [rsp+20h],eax
00007FFBAB3BD4B9 89542424             mov       [rsp+24h],edx
00007FFBAB3BD4BD 894C2428             mov       [rsp+28h],ecx
00007FFBAB3BD4C1 C5F910442420         vmovupd   xmm0,[rsp+20h]
00007FFBAB3BD4C7 C5F911442430         vmovupd   [rsp+30h],xmm0
00007FFBAB3BD4CD 8B442430             mov       eax,[rsp+30h]
00007FFBAB3BD4D1 8B542434             mov       edx,[rsp+34h]
00007FFBAB3BD4D5 8B4C2438             mov       ecx,[rsp+38h]
00007FFBAB3BD4D9 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3BD4DE 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3BD4E3 0F8CEAFDFFFF         jl        near 0000`7FFB`AB3B`D2D3h
00007FFBAB3BD4E9 0FAFC2               imul      eax,edx
00007FFBAB3BD4EC 4883C458             add       rsp,58h
00007FFBAB3BD4F0 C3                   ret
00007FFBAB3BD4F1 E85A12A35F           call      0000`7FFC`0ADE`E750h
00007FFBAB3BD4F6 CC                   int3
```
### Int32 PartII(): Tier1
```assembly
00007FFBAB3D96E0 4883EC58             sub       rsp,58h
00007FFBAB3D96E4 C5F877               vzeroupper
00007FFBAB3D96E7 C4E15857E4           vxorps    xmm4,xmm4,xmm4
00007FFBAB3D96EC C4E1797F642440       vmovdqa   [rsp+40h],xmm4
00007FFBAB3D96F3 33C0                 xor       eax,eax
00007FFBAB3D96F5 4889442450           mov       [rsp+50h],rax
00007FFBAB3D96FA 488B4108             mov       rax,[rcx+8]
00007FFBAB3D96FE 4885C0               test      rax,rax
00007FFBAB3D9701 7506                 jne       short 0000`7FFB`AB3D`9709h
00007FFBAB3D9703 33D2                 xor       edx,edx
00007FFBAB3D9705 33C9                 xor       ecx,ecx
00007FFBAB3D9707 EB07                 jmp       short 0000`7FFB`AB3D`9710h
00007FFBAB3D9709 488D500C             lea       rdx,[rax+0Ch]
00007FFBAB3D970D 8B4808               mov       ecx,[rax+8]
00007FFBAB3D9710 488D442440           lea       rax,[rsp+40h]
00007FFBAB3D9715 488910               mov       [rax],rdx
00007FFBAB3D9718 894808               mov       [rax+8],ecx
00007FFBAB3D971B 33C0                 xor       eax,eax
00007FFBAB3D971D 89442450             mov       [rsp+50h],eax
00007FFBAB3D9721 33D2                 xor       edx,edx
00007FFBAB3D9723 33C9                 xor       ecx,ecx
00007FFBAB3D9725 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D972A 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3D972F 0F8D1F020000         jge       near 0000`7FFB`AB3D`9954h
00007FFBAB3D9735 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D973A 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D973F 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D9743 0F8313020000         jae       near 0000`7FFB`AB3D`995Ch
00007FFBAB3D9749 4D8B09               mov       r9,[r9]
00007FFBAB3D974C 4D63C0               movsxd    r8,r8d
00007FFBAB3D974F 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D9754 4183F864             cmp       r8d,64h
00007FFBAB3D9758 0F84ED000000         je        near 0000`7FFB`AB3D`984Bh
00007FFBAB3D975E 4183F866             cmp       r8d,66h
00007FFBAB3D9762 7450                 je        short 0000`7FFB`AB3D`97B4h
00007FFBAB3D9764 4183F875             cmp       r8d,75h
00007FFBAB3D9768 0F85A8010000         jne       near 0000`7FFB`AB3D`9916h
00007FFBAB3D976E 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9773 4183C003             add       r8d,3
00007FFBAB3D9777 4489442450           mov       [rsp+50h],r8d
00007FFBAB3D977C 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9781 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D9786 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D978A 0F83CC010000         jae       near 0000`7FFB`AB3D`995Ch
00007FFBAB3D9790 4D8B09               mov       r9,[r9]
00007FFBAB3D9793 4D63C0               movsxd    r8,r8d
00007FFBAB3D9796 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D979B 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3D979F 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3D97A4 41FFC1               inc       r9d
00007FFBAB3D97A7 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3D97AC 412BC8               sub       ecx,r8d
00007FFBAB3D97AF E92F010000           jmp       0000`7FFB`AB3D`98E3h
00007FFBAB3D97B4 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D97B9 4183C008             add       r8d,8
00007FFBAB3D97BD 4489442450           mov       [rsp+50h],r8d
00007FFBAB3D97C2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D97C7 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D97CC 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D97D0 0F8386010000         jae       near 0000`7FFB`AB3D`995Ch
00007FFBAB3D97D6 4D8B09               mov       r9,[r9]
00007FFBAB3D97D9 4D63C0               movsxd    r8,r8d
00007FFBAB3D97DC 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D97E1 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3D97E5 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3D97EA 41FFC1               inc       r9d
00007FFBAB3D97ED 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3D97F2 4103C0               add       eax,r8d
00007FFBAB3D97F5 440FAFC1             imul      r8d,ecx
00007FFBAB3D97F9 4103D0               add       edx,r8d
00007FFBAB3D97FC EB0D                 jmp       short 0000`7FFB`AB3D`980Bh
00007FFBAB3D97FE 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9803 41FFC0               inc       r8d
00007FFBAB3D9806 4489442450           mov       [rsp+50h],r8d
00007FFBAB3D980B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9810 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3D9815 0F8DFB000000         jge       near 0000`7FFB`AB3D`9916h
00007FFBAB3D981B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9820 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D9825 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D9829 0F832D010000         jae       near 0000`7FFB`AB3D`995Ch
00007FFBAB3D982F 4D8B09               mov       r9,[r9]
00007FFBAB3D9832 4D63C0               movsxd    r8,r8d
00007FFBAB3D9835 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D983A 4183F861             cmp       r8d,61h
00007FFBAB3D983E 7CBE                 jl        short 0000`7FFB`AB3D`97FEh
00007FFBAB3D9840 4183F87A             cmp       r8d,7Ah
00007FFBAB3D9844 7FB8                 jg        short 0000`7FFB`AB3D`97FEh
00007FFBAB3D9846 E9CB000000           jmp       0000`7FFB`AB3D`9916h
00007FFBAB3D984B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9850 4183C005             add       r8d,5
00007FFBAB3D9854 4489442450           mov       [rsp+50h],r8d
00007FFBAB3D9859 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D985E 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D9863 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D9867 0F83EF000000         jae       near 0000`7FFB`AB3D`995Ch
00007FFBAB3D986D 4D8B09               mov       r9,[r9]
00007FFBAB3D9870 4D63C0               movsxd    r8,r8d
00007FFBAB3D9873 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D9878 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3D987C 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3D9881 41FFC1               inc       r9d
00007FFBAB3D9884 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3D9889 4103C8               add       ecx,r8d
00007FFBAB3D988C EB0F                 jmp       short 0000`7FFB`AB3D`989Dh
00007FFBAB3D988E 6690                 xchg      ax,ax
00007FFBAB3D9890 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9895 41FFC0               inc       r8d
00007FFBAB3D9898 4489442450           mov       [rsp+50h],r8d
00007FFBAB3D989D 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D98A2 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3D98A7 7D6D                 jge       short 0000`7FFB`AB3D`9916h
00007FFBAB3D98A9 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D98AE 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D98B3 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D98B7 0F839F000000         jae       near 0000`7FFB`AB3D`995Ch
00007FFBAB3D98BD 4D8B09               mov       r9,[r9]
00007FFBAB3D98C0 4D63C0               movsxd    r8,r8d
00007FFBAB3D98C3 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D98C8 4183F861             cmp       r8d,61h
00007FFBAB3D98CC 7CC2                 jl        short 0000`7FFB`AB3D`9890h
00007FFBAB3D98CE 4183F87A             cmp       r8d,7Ah
00007FFBAB3D98D2 7FBC                 jg        short 0000`7FFB`AB3D`9890h
00007FFBAB3D98D4 EB40                 jmp       short 0000`7FFB`AB3D`9916h
00007FFBAB3D98D6 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D98DB 41FFC0               inc       r8d
00007FFBAB3D98DE 4489442450           mov       [rsp+50h],r8d
00007FFBAB3D98E3 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D98E8 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3D98ED 7D27                 jge       short 0000`7FFB`AB3D`9916h
00007FFBAB3D98EF 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D98F4 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3D98F9 453B4108             cmp       r8d,[r9+8]
00007FFBAB3D98FD 735D                 jae       short 0000`7FFB`AB3D`995Ch
00007FFBAB3D98FF 4D8B09               mov       r9,[r9]
00007FFBAB3D9902 4D63C0               movsxd    r8,r8d
00007FFBAB3D9905 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3D990A 4183F861             cmp       r8d,61h
00007FFBAB3D990E 7CC6                 jl        short 0000`7FFB`AB3D`98D6h
00007FFBAB3D9910 4183F87A             cmp       r8d,7Ah
00007FFBAB3D9914 7FC0                 jg        short 0000`7FFB`AB3D`98D6h
00007FFBAB3D9916 C5F857C0             vxorps    xmm0,xmm0,xmm0
00007FFBAB3D991A C5F911442420         vmovupd   [rsp+20h],xmm0
00007FFBAB3D9920 89442420             mov       [rsp+20h],eax
00007FFBAB3D9924 89542424             mov       [rsp+24h],edx
00007FFBAB3D9928 894C2428             mov       [rsp+28h],ecx
00007FFBAB3D992C C5F910442420         vmovupd   xmm0,[rsp+20h]
00007FFBAB3D9932 C5F911442430         vmovupd   [rsp+30h],xmm0
00007FFBAB3D9938 8B442430             mov       eax,[rsp+30h]
00007FFBAB3D993C 8B542434             mov       edx,[rsp+34h]
00007FFBAB3D9940 8B4C2438             mov       ecx,[rsp+38h]
00007FFBAB3D9944 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3D9949 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3D994E 0F8CE1FDFFFF         jl        near 0000`7FFB`AB3D`9735h
00007FFBAB3D9954 0FAFC2               imul      eax,edx
00007FFBAB3D9957 4883C458             add       rsp,58h
00007FFBAB3D995B C3                   ret
00007FFBAB3D995C E8EF4DA15F           call      0000`7FFC`0ADE`E750h
00007FFBAB3D9961 CC                   int3
```
### Int32 PartII_ptr(): Tier1
```assembly
00007FFBAB3DA8A0 4883EC28             sub       rsp,28h
00007FFBAB3DA8A4 C5F877               vzeroupper
00007FFBAB3DA8A7 33C0                 xor       eax,eax
00007FFBAB3DA8A9 4889442420           mov       [rsp+20h],rax
00007FFBAB3DA8AE 488B4108             mov       rax,[rcx+8]
00007FFBAB3DA8B2 488BD0               mov       rdx,rax
00007FFBAB3DA8B5 4885D2               test      rdx,rdx
00007FFBAB3DA8B8 7504                 jne       short 0000`7FFB`AB3D`A8BEh
00007FFBAB3DA8BA 33C9                 xor       ecx,ecx
00007FFBAB3DA8BC EB0E                 jmp       short 0000`7FFB`AB3D`A8CCh
00007FFBAB3DA8BE 4883C20C             add       rdx,0Ch
00007FFBAB3DA8C2 4889542420           mov       [rsp+20h],rdx
00007FFBAB3DA8C7 488B4C2420           mov       rcx,[rsp+20h]
00007FFBAB3DA8CC 8B4008               mov       eax,[rax+8]
00007FFBAB3DA8CF 33D2                 xor       edx,edx
00007FFBAB3DA8D1 4533C0               xor       r8d,r8d
00007FFBAB3DA8D4 4533C9               xor       r9d,r9d
00007FFBAB3DA8D7 4533D2               xor       r10d,r10d
00007FFBAB3DA8DA 85C0                 test      eax,eax
00007FFBAB3DA8DC 0F8E0D010000         jle       near 0000`7FFB`AB3D`A9EFh
00007FFBAB3DA8E2 4C63DA               movsxd    r11,edx
00007FFBAB3DA8E5 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA8EA 4183FB64             cmp       r11d,64h
00007FFBAB3DA8EE 746C                 je        short 0000`7FFB`AB3D`A95Ch
00007FFBAB3DA8F0 4183FB66             cmp       r11d,66h
00007FFBAB3DA8F4 7423                 je        short 0000`7FFB`AB3D`A919h
00007FFBAB3DA8F6 4183FB75             cmp       r11d,75h
00007FFBAB3DA8FA 0F85B6000000         jne       near 0000`7FFB`AB3D`A9B6h
00007FFBAB3DA900 83C203               add       edx,3
00007FFBAB3DA903 4C63DA               movsxd    r11,edx
00007FFBAB3DA906 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA90B 4183C3D0             add       r11d,0`FFFF`FFD0h
00007FFBAB3DA90F FFC2                 inc       edx
00007FFBAB3DA911 452BD3               sub       r10d,r11d
00007FFBAB3DA914 E985000000           jmp       0000`7FFB`AB3D`A99Eh
00007FFBAB3DA919 83C208               add       edx,8
00007FFBAB3DA91C 4C63DA               movsxd    r11,edx
00007FFBAB3DA91F 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA924 4183C3D0             add       r11d,0`FFFF`FFD0h
00007FFBAB3DA928 FFC2                 inc       edx
00007FFBAB3DA92A 4503C3               add       r8d,r11d
00007FFBAB3DA92D 450FAFDA             imul      r11d,r10d
00007FFBAB3DA931 4503CB               add       r9d,r11d
00007FFBAB3DA934 EB0C                 jmp       short 0000`7FFB`AB3D`A942h
00007FFBAB3DA936 66660F1F840000000000 nop       word [rax+rax]
00007FFBAB3DA940 FFC2                 inc       edx
00007FFBAB3DA942 3BD0                 cmp       edx,eax
00007FFBAB3DA944 7D70                 jge       short 0000`7FFB`AB3D`A9B6h
00007FFBAB3DA946 4C63DA               movsxd    r11,edx
00007FFBAB3DA949 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA94E 4183FB61             cmp       r11d,61h
00007FFBAB3DA952 7CEC                 jl        short 0000`7FFB`AB3D`A940h
00007FFBAB3DA954 4183FB7A             cmp       r11d,7Ah
00007FFBAB3DA958 7FE6                 jg        short 0000`7FFB`AB3D`A940h
00007FFBAB3DA95A EB5A                 jmp       short 0000`7FFB`AB3D`A9B6h
00007FFBAB3DA95C 83C205               add       edx,5
00007FFBAB3DA95F 4C63DA               movsxd    r11,edx
00007FFBAB3DA962 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA967 4183C3D0             add       r11d,0`FFFF`FFD0h
00007FFBAB3DA96B FFC2                 inc       edx
00007FFBAB3DA96D 4503D3               add       r10d,r11d
00007FFBAB3DA970 EB10                 jmp       short 0000`7FFB`AB3D`A982h
00007FFBAB3DA972 0F1F8000000000       nop       dword [rax]
00007FFBAB3DA979 0F1F8000000000       nop       dword [rax]
00007FFBAB3DA980 FFC2                 inc       edx
00007FFBAB3DA982 3BD0                 cmp       edx,eax
00007FFBAB3DA984 7D30                 jge       short 0000`7FFB`AB3D`A9B6h
00007FFBAB3DA986 4C63DA               movsxd    r11,edx
00007FFBAB3DA989 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA98E 4183FB61             cmp       r11d,61h
00007FFBAB3DA992 7CEC                 jl        short 0000`7FFB`AB3D`A980h
00007FFBAB3DA994 4183FB7A             cmp       r11d,7Ah
00007FFBAB3DA998 7FE6                 jg        short 0000`7FFB`AB3D`A980h
00007FFBAB3DA99A EB1A                 jmp       short 0000`7FFB`AB3D`A9B6h
00007FFBAB3DA99C FFC2                 inc       edx
00007FFBAB3DA99E 3BD0                 cmp       edx,eax
00007FFBAB3DA9A0 7D14                 jge       short 0000`7FFB`AB3D`A9B6h
00007FFBAB3DA9A2 4C63DA               movsxd    r11,edx
00007FFBAB3DA9A5 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA9AA 4183FB61             cmp       r11d,61h
00007FFBAB3DA9AE 7CEC                 jl        short 0000`7FFB`AB3D`A99Ch
00007FFBAB3DA9B0 4183FB7A             cmp       r11d,7Ah
00007FFBAB3DA9B4 7FE6                 jg        short 0000`7FFB`AB3D`A99Ch
00007FFBAB3DA9B6 C5F857C0             vxorps    xmm0,xmm0,xmm0
00007FFBAB3DA9BA C5F9110424           vmovupd   [rsp],xmm0
00007FFBAB3DA9BF 44890424             mov       [rsp],r8d
00007FFBAB3DA9C3 44894C2404           mov       [rsp+4],r9d
00007FFBAB3DA9C8 4489542408           mov       [rsp+8],r10d
00007FFBAB3DA9CD C5F9100424           vmovupd   xmm0,[rsp]
00007FFBAB3DA9D2 C5F911442410         vmovupd   [rsp+10h],xmm0
00007FFBAB3DA9D8 448B442410           mov       r8d,[rsp+10h]
00007FFBAB3DA9DD 448B4C2414           mov       r9d,[rsp+14h]
00007FFBAB3DA9E2 448B542418           mov       r10d,[rsp+18h]
00007FFBAB3DA9E7 3BD0                 cmp       edx,eax
00007FFBAB3DA9E9 0F8CF3FEFFFF         jl        near 0000`7FFB`AB3D`A8E2h
00007FFBAB3DA9EF 418BC0               mov       eax,r8d
00007FFBAB3DA9F2 410FAFC1             imul      eax,r9d
00007FFBAB3DA9F6 4883C428             add       rsp,28h
00007FFBAB3DA9FA C3                   ret
```
### Int32 PartI_ptr(): Tier1
```assembly
00007FFBAB3DA360 4883EC28             sub       rsp,28h
00007FFBAB3DA364 C5F877               vzeroupper
00007FFBAB3DA367 33C0                 xor       eax,eax
00007FFBAB3DA369 4889442420           mov       [rsp+20h],rax
00007FFBAB3DA36E 488B4108             mov       rax,[rcx+8]
00007FFBAB3DA372 488BD0               mov       rdx,rax
00007FFBAB3DA375 4885D2               test      rdx,rdx
00007FFBAB3DA378 7504                 jne       short 0000`7FFB`AB3D`A37Eh
00007FFBAB3DA37A 33C9                 xor       ecx,ecx
00007FFBAB3DA37C EB0E                 jmp       short 0000`7FFB`AB3D`A38Ch
00007FFBAB3DA37E 4883C20C             add       rdx,0Ch
00007FFBAB3DA382 4889542420           mov       [rsp+20h],rdx
00007FFBAB3DA387 488B4C2420           mov       rcx,[rsp+20h]
00007FFBAB3DA38C 8B4008               mov       eax,[rax+8]
00007FFBAB3DA38F 33D2                 xor       edx,edx
00007FFBAB3DA391 4533C0               xor       r8d,r8d
00007FFBAB3DA394 4533C9               xor       r9d,r9d
00007FFBAB3DA397 4533D2               xor       r10d,r10d
00007FFBAB3DA39A 85C0                 test      eax,eax
00007FFBAB3DA39C 0F8EEF000000         jle       near 0000`7FFB`AB3D`A491h
00007FFBAB3DA3A2 4C63DA               movsxd    r11,edx
00007FFBAB3DA3A5 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA3AA 4183FB64             cmp       r11d,64h
00007FFBAB3DA3AE 745C                 je        short 0000`7FFB`AB3D`A40Ch
00007FFBAB3DA3B0 4183FB66             cmp       r11d,66h
00007FFBAB3DA3B4 7423                 je        short 0000`7FFB`AB3D`A3D9h
00007FFBAB3DA3B6 4183FB75             cmp       r11d,75h
00007FFBAB3DA3BA 0F8598000000         jne       near 0000`7FFB`AB3D`A458h
00007FFBAB3DA3C0 83C203               add       edx,3
00007FFBAB3DA3C3 4C63DA               movsxd    r11,edx
00007FFBAB3DA3C6 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA3CB 4183C3D0             add       r11d,0`FFFF`FFD0h
00007FFBAB3DA3CF FFC2                 inc       edx
00007FFBAB3DA3D1 452BCB               sub       r9d,r11d
00007FFBAB3DA3D4 EB6A                 jmp       short 0000`7FFB`AB3D`A440h
00007FFBAB3DA3D6 0F1F00               nop       dword [rax]
00007FFBAB3DA3D9 83C208               add       edx,8
00007FFBAB3DA3DC 4C63DA               movsxd    r11,edx
00007FFBAB3DA3DF 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA3E4 4183C3D0             add       r11d,0`FFFF`FFD0h
00007FFBAB3DA3E8 FFC2                 inc       edx
00007FFBAB3DA3EA 4503C3               add       r8d,r11d
00007FFBAB3DA3ED EB03                 jmp       short 0000`7FFB`AB3D`A3F2h
00007FFBAB3DA3EF 90                   nop
00007FFBAB3DA3F0 FFC2                 inc       edx
00007FFBAB3DA3F2 3BD0                 cmp       edx,eax
00007FFBAB3DA3F4 7D62                 jge       short 0000`7FFB`AB3D`A458h
00007FFBAB3DA3F6 4C63DA               movsxd    r11,edx
00007FFBAB3DA3F9 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA3FE 4183FB61             cmp       r11d,61h
00007FFBAB3DA402 7CEC                 jl        short 0000`7FFB`AB3D`A3F0h
00007FFBAB3DA404 4183FB7A             cmp       r11d,7Ah
00007FFBAB3DA408 7FE6                 jg        short 0000`7FFB`AB3D`A3F0h
00007FFBAB3DA40A EB4C                 jmp       short 0000`7FFB`AB3D`A458h
00007FFBAB3DA40C 83C205               add       edx,5
00007FFBAB3DA40F 4C63DA               movsxd    r11,edx
00007FFBAB3DA412 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA417 4183C3D0             add       r11d,0`FFFF`FFD0h
00007FFBAB3DA41B FFC2                 inc       edx
00007FFBAB3DA41D 4503CB               add       r9d,r11d
00007FFBAB3DA420 EB02                 jmp       short 0000`7FFB`AB3D`A424h
00007FFBAB3DA422 FFC2                 inc       edx
00007FFBAB3DA424 3BD0                 cmp       edx,eax
00007FFBAB3DA426 7D30                 jge       short 0000`7FFB`AB3D`A458h
00007FFBAB3DA428 4C63DA               movsxd    r11,edx
00007FFBAB3DA42B 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA430 4183FB61             cmp       r11d,61h
00007FFBAB3DA434 7CEC                 jl        short 0000`7FFB`AB3D`A422h
00007FFBAB3DA436 4183FB7A             cmp       r11d,7Ah
00007FFBAB3DA43A 7FE6                 jg        short 0000`7FFB`AB3D`A422h
00007FFBAB3DA43C EB1A                 jmp       short 0000`7FFB`AB3D`A458h
00007FFBAB3DA43E FFC2                 inc       edx
00007FFBAB3DA440 3BD0                 cmp       edx,eax
00007FFBAB3DA442 7D14                 jge       short 0000`7FFB`AB3D`A458h
00007FFBAB3DA444 4C63DA               movsxd    r11,edx
00007FFBAB3DA447 460FB71C59           movzx     r11d,word [rcx+r11*2]
00007FFBAB3DA44C 4183FB61             cmp       r11d,61h
00007FFBAB3DA450 7CEC                 jl        short 0000`7FFB`AB3D`A43Eh
00007FFBAB3DA452 4183FB7A             cmp       r11d,7Ah
00007FFBAB3DA456 7FE6                 jg        short 0000`7FFB`AB3D`A43Eh
00007FFBAB3DA458 C5F857C0             vxorps    xmm0,xmm0,xmm0
00007FFBAB3DA45C C5F9110424           vmovupd   [rsp],xmm0
00007FFBAB3DA461 44890424             mov       [rsp],r8d
00007FFBAB3DA465 44894C2404           mov       [rsp+4],r9d
00007FFBAB3DA46A 4489542408           mov       [rsp+8],r10d
00007FFBAB3DA46F C5F9100424           vmovupd   xmm0,[rsp]
00007FFBAB3DA474 C5F911442410         vmovupd   [rsp+10h],xmm0
00007FFBAB3DA47A 448B442410           mov       r8d,[rsp+10h]
00007FFBAB3DA47F 448B4C2414           mov       r9d,[rsp+14h]
00007FFBAB3DA484 448B542418           mov       r10d,[rsp+18h]
00007FFBAB3DA489 3BD0                 cmp       edx,eax
00007FFBAB3DA48B 0F8C11FFFFFF         jl        near 0000`7FFB`AB3D`A3A2h
00007FFBAB3DA491 418BC0               mov       eax,r8d
00007FFBAB3DA494 410FAFC1             imul      eax,r9d
00007FFBAB3DA498 4883C428             add       rsp,28h
00007FFBAB3DA49C C3                   ret
```
