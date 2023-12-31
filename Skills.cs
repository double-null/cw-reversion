using System;
using System.Reflection;

// Token: 0x0200026B RID: 619
[Obfuscation(Exclude = true)]
public enum Skills
{
	// Token: 0x040012EE RID: 4846
	none = -1,
	// Token: 0x040012EF RID: 4847
	analyze1,
	// Token: 0x040012F0 RID: 4848
	analyze2,
	// Token: 0x040012F1 RID: 4849
	scan,
	// Token: 0x040012F2 RID: 4850
	analyze3,
	// Token: 0x040012F3 RID: 4851
	accr_p,
	// Token: 0x040012F4 RID: 4852
	mob1,
	// Token: 0x040012F5 RID: 4853
	sitspeed,
	// Token: 0x040012F6 RID: 4854
	rec_p,
	// Token: 0x040012F7 RID: 4855
	rel1,
	// Token: 0x040012F8 RID: 4856
	operator_pp,
	// Token: 0x040012F9 RID: 4857
	rel_p,
	// Token: 0x040012FA RID: 4858
	mob2,
	// Token: 0x040012FB RID: 4859
	ammo_p,
	// Token: 0x040012FC RID: 4860
	ammo_pp,
	// Token: 0x040012FD RID: 4861
	dam_p,
	// Token: 0x040012FE RID: 4862
	knife,
	// Token: 0x040012FF RID: 4863
	mob3,
	// Token: 0x04001300 RID: 4864
	rec2_p,
	// Token: 0x04001301 RID: 4865
	longb1,
	// Token: 0x04001302 RID: 4866
	mast_p,
	// Token: 0x04001303 RID: 4867
	firerate,
	// Token: 0x04001304 RID: 4868
	mast_scout,
	// Token: 0x04001305 RID: 4869
	uniq_p,
	// Token: 0x04001306 RID: 4870
	silwalk,
	// Token: 0x04001307 RID: 4871
	uniq_pp,
	// Token: 0x04001308 RID: 4872
	hp5_scout,
	// Token: 0x04001309 RID: 4873
	arm_limbs,
	// Token: 0x0400130A RID: 4874
	fall,
	// Token: 0x0400130B RID: 4875
	regen1,
	// Token: 0x0400130C RID: 4876
	regen2,
	// Token: 0x0400130D RID: 4877
	arm1,
	// Token: 0x0400130E RID: 4878
	arm2,
	// Token: 0x0400130F RID: 4879
	arm3,
	// Token: 0x04001310 RID: 4880
	arm4,
	// Token: 0x04001311 RID: 4881
	arm5,
	// Token: 0x04001312 RID: 4882
	rel2,
	// Token: 0x04001313 RID: 4883
	longb2,
	// Token: 0x04001314 RID: 4884
	rec1,
	// Token: 0x04001315 RID: 4885
	hp5_assault,
	// Token: 0x04001316 RID: 4886
	ammo_shot,
	// Token: 0x04001317 RID: 4887
	ammo_rifle,
	// Token: 0x04001318 RID: 4888
	penetr,
	// Token: 0x04001319 RID: 4889
	rel3,
	// Token: 0x0400131A RID: 4890
	operator_storm,
	// Token: 0x0400131B RID: 4891
	dam1,
	// Token: 0x0400131C RID: 4892
	dam2,
	// Token: 0x0400131D RID: 4893
	mob_shot,
	// Token: 0x0400131E RID: 4894
	mob_rifle,
	// Token: 0x0400131F RID: 4895
	stab_rifle,
	// Token: 0x04001320 RID: 4896
	mast_storm,
	// Token: 0x04001321 RID: 4897
	uniq_shot,
	// Token: 0x04001322 RID: 4898
	uniq_rifle,
	// Token: 0x04001323 RID: 4899
	car_hcunlock,
	// Token: 0x04001324 RID: 4900
	rel2_shot,
	// Token: 0x04001325 RID: 4901
	att1,
	// Token: 0x04001326 RID: 4902
	att2,
	// Token: 0x04001327 RID: 4903
	att3,
	// Token: 0x04001328 RID: 4904
	att4,
	// Token: 0x04001329 RID: 4905
	att5,
	// Token: 0x0400132A RID: 4906
	hear1,
	// Token: 0x0400132B RID: 4907
	hear2,
	// Token: 0x0400132C RID: 4908
	hear3,
	// Token: 0x0400132D RID: 4909
	conc,
	// Token: 0x0400132E RID: 4910
	swalk_sniper,
	// Token: 0x0400132F RID: 4911
	accr1,
	// Token: 0x04001330 RID: 4912
	aim_rec,
	// Token: 0x04001331 RID: 4913
	walk_accr,
	// Token: 0x04001332 RID: 4914
	rel_sni2,
	// Token: 0x04001333 RID: 4915
	operator_sni,
	// Token: 0x04001334 RID: 4916
	accr2,
	// Token: 0x04001335 RID: 4917
	marks,
	// Token: 0x04001336 RID: 4918
	accr3,
	// Token: 0x04001337 RID: 4919
	dam3,
	// Token: 0x04001338 RID: 4920
	longb_sn,
	// Token: 0x04001339 RID: 4921
	mast_sni,
	// Token: 0x0400133A RID: 4922
	sit_accr,
	// Token: 0x0400133B RID: 4923
	pro_sn,
	// Token: 0x0400133C RID: 4924
	uniq_sni,
	// Token: 0x0400133D RID: 4925
	hp5,
	// Token: 0x0400133E RID: 4926
	des1,
	// Token: 0x0400133F RID: 4927
	des2,
	// Token: 0x04001340 RID: 4928
	des3,
	// Token: 0x04001341 RID: 4929
	sonar1,
	// Token: 0x04001342 RID: 4930
	sonar2,
	// Token: 0x04001343 RID: 4931
	sonar3,
	// Token: 0x04001344 RID: 4932
	mortar1,
	// Token: 0x04001345 RID: 4933
	mortar2,
	// Token: 0x04001346 RID: 4934
	mortar3,
	// Token: 0x04001347 RID: 4935
	mob_mods,
	// Token: 0x04001348 RID: 4936
	rep1,
	// Token: 0x04001349 RID: 4937
	rep2,
	// Token: 0x0400134A RID: 4938
	rep3,
	// Token: 0x0400134B RID: 4939
	rep4,
	// Token: 0x0400134C RID: 4940
	unbreak,
	// Token: 0x0400134D RID: 4941
	colim_accr,
	// Token: 0x0400134E RID: 4942
	rec_mods,
	// Token: 0x0400134F RID: 4943
	colim_zoom,
	// Token: 0x04001350 RID: 4944
	optics_accr,
	// Token: 0x04001351 RID: 4945
	mob_sil,
	// Token: 0x04001352 RID: 4946
	nodam_sil,
	// Token: 0x04001353 RID: 4947
	accr_sil,
	// Token: 0x04001354 RID: 4948
	apammo,
	// Token: 0x04001355 RID: 4949
	nvg,
	// Token: 0x04001356 RID: 4950
	fragarmor,
	// Token: 0x04001357 RID: 4951
	aimspeed1,
	// Token: 0x04001358 RID: 4952
	aimspeed2,
	// Token: 0x04001359 RID: 4953
	rec2,
	// Token: 0x0400135A RID: 4954
	rel_heavy,
	// Token: 0x0400135B RID: 4955
	sit_rec,
	// Token: 0x0400135C RID: 4956
	rel_mg1,
	// Token: 0x0400135D RID: 4957
	rel_mg2,
	// Token: 0x0400135E RID: 4958
	ammo,
	// Token: 0x0400135F RID: 4959
	rec3,
	// Token: 0x04001360 RID: 4960
	hp10,
	// Token: 0x04001361 RID: 4961
	armor10,
	// Token: 0x04001362 RID: 4962
	frag_id,
	// Token: 0x04001363 RID: 4963
	longb_mg,
	// Token: 0x04001364 RID: 4964
	mob_mg,
	// Token: 0x04001365 RID: 4965
	mob_mg2,
	// Token: 0x04001366 RID: 4966
	operator_mg,
	// Token: 0x04001367 RID: 4967
	stab,
	// Token: 0x04001368 RID: 4968
	mast_dest,
	// Token: 0x04001369 RID: 4969
	uniq_mg,
	// Token: 0x0400136A RID: 4970
	efd,
	// Token: 0x0400136B RID: 4971
	efd2,
	// Token: 0x0400136C RID: 4972
	efd_throw,
	// Token: 0x0400136D RID: 4973
	efd_dam,
	// Token: 0x0400136E RID: 4974
	efd_radius,
	// Token: 0x0400136F RID: 4975
	stopper,
	// Token: 0x04001370 RID: 4976
	stop_ammo,
	// Token: 0x04001371 RID: 4977
	car_block,
	// Token: 0x04001372 RID: 4978
	car_remove,
	// Token: 0x04001373 RID: 4979
	car_suicide,
	// Token: 0x04001374 RID: 4980
	car_streak12,
	// Token: 0x04001375 RID: 4981
	car_streak2,
	// Token: 0x04001376 RID: 4982
	car_night2,
	// Token: 0x04001377 RID: 4983
	car_expbonus1,
	// Token: 0x04001378 RID: 4984
	car_expbonus2,
	// Token: 0x04001379 RID: 4985
	car_expbonus3,
	// Token: 0x0400137A RID: 4986
	car_contr,
	// Token: 0x0400137B RID: 4987
	car_exp2,
	// Token: 0x0400137C RID: 4988
	car_exp3,
	// Token: 0x0400137D RID: 4989
	car_wtask,
	// Token: 0x0400137E RID: 4990
	car_ach,
	// Token: 0x0400137F RID: 4991
	car_usec,
	// Token: 0x04001380 RID: 4992
	car_bear,
	// Token: 0x04001381 RID: 4993
	car_sp,
	// Token: 0x04001382 RID: 4994
	car_health,
	// Token: 0x04001383 RID: 4995
	car_night,
	// Token: 0x04001384 RID: 4996
	car_weap,
	// Token: 0x04001385 RID: 4997
	car_immortal,
	// Token: 0x04001386 RID: 4998
	car_respawn,
	// Token: 0x04001387 RID: 4999
	spec_mp5,
	// Token: 0x04001388 RID: 5000
	lp_gain,
	// Token: 0x04001389 RID: 5001
	lp_protect,
	// Token: 0x0400138A RID: 5002
	last
}
