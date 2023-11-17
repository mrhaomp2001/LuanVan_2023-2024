<?php

namespace App\Livewire\Admins\Users;

use App\Models\User;
use Livewire\Component;

class AdminUsersShow extends Component
{
    public $user;
    public $id;
    public $name;
    public $username;
    public $avatar_path;
    public $is_ban;
    public function mount($user_id) {
        $this->user = User::findOrFail($user_id);
        $this->id =  $this->user->id;
        $this->name = $this->user->name;
        $this->username = $this->user->username;
        $this->avatar_path = $this->user->avatar_path;
        $this->is_ban = $this->user->is_ban;

    }
    public function save() {
        $this->user->is_ban = !$this->user->is_ban;
        $this->user->save();
        $this->redirect(route("admin.user.index"));
    }
    public function render()
    {
        return view('livewire.admins.users.admin-users-show');
    }
}
