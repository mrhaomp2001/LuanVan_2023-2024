<?php

namespace App\Livewire\Admins\SystemNotice;

use App\Models\SystemNotification;
use Livewire\Component;

class AdminSystemNoticeShow extends Component
{
    public $notification;
    public $content;
    public $can_use;

    public function mount($notification_id)
    {
        $this->notification = SystemNotification::findOrFail($notification_id);

        $this->content = $this->notification->content;
        $this->can_use = $this->notification->can_use;
    }

    public function save()
    {
        $this->notification->content = $this->content;
        $this->notification->can_use = $this->can_use;

        $this->notification->save();

        $this->redirect(route("admin.system-notification.index"));
    }
    public function render()
    {
        return view('livewire.admins.system-notice.admin-system-notice-show');
    }
}
