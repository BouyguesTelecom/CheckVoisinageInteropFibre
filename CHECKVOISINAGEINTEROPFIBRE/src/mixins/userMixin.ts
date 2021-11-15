import { User } from "@/types/DTO/user"
import { Getters } from '@/store';

export const UserMixin = {
    mounted(){
        const delay = (ms: number) => new Promise(res => setTimeout(res, ms));
        this.$store.watch(
            (_: void, getter: Getters) => getter.getCurrentUser,
            async (newVal: User) => {
                if (newVal && newVal.login != undefined && newVal.login != ''){
                    this.currentUser = newVal;
                    //DELAY  C'EST IMPORTANT POUR GENERATION DES DONNEES.
                    await delay(1200);
                    if(this.$router.history.current.name === "Register") {
                        this.$router.push({ name: 'Home' })
                    }
                }
            }
          )
    },
    data()
    {
        return {
            currentUser: {} as User
        }
    }
  }